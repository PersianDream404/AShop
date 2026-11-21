# راهنمای فرانت‌اند AShop

این راهنما بر اساس API فعلی پروژه نوشته شده و مسیر کاربر از ورود به پنل تا ثبت سفارش و پرداخت را توضیح می‌دهد.

## تنظیمات پایه

- `BASE_URL` را برابر آدرس اجرای Web API قرار دهید؛ برای مثال `https://localhost:5001`.
- مسیرهای API نسخه‌بندی نشده‌اند.
- بیشتر endpointها پاسخ را در قالب زیر برمی‌گردانند:

```json
{
  "data": {},
  "message": null,
  "errors": [],
  "statusCode": 200
}
```

در مثال‌های این سند، مقدار موردنیاز فرانت داخل `data` قرار دارد. پاسخ بعضی خطاها یا redirect پرداخت ممکن است این قالب را نداشته باشد.

## ورود و پنل کاربر

### ثبت‌نام

`POST /api/Account/Register`

```json
{
  "mobile": "09123456789",
  "password": "your-password"
}
```

موبایل الزامی است و رمز حداقل ۳ کاراکتر دارد. پاسخ شامل `data.tokenResult.token` و `data.info` است. شناسه کاربر در `data.info.id` قرار دارد.

### ورود

`POST /api/Account/Login`

```json
{
  "userName": "09123456789",
  "password": "your-password"
}
```

در حال حاضر مقدار `userName` همان شماره موبایل است. پاسخ:

```json
{
  "data": {
    "tokenResult": {
      "token": "...",
      "expireDate": "..."
    },
    "info": {
      "id": 123,
      "roles": []
    }
  },
  "message": null,
  "errors": [],
  "statusCode": 200
}
```

توکن را امن نگه دارید و برای endpointهای نیازمند ورود ارسال کنید:

```http
Authorization: Bearer <token>
```

### مدیریت نشانی‌ها

همه مسیرهای زیر نیازمند Bearer token هستند و شناسه کاربر را از توکن می‌گیرند:

| عملیات | روش و مسیر |
|---|---|
| دریافت نشانی‌های من | `GET /api/Account/Address/Addresses` |
| افزودن نشانی | `POST /api/Account/Address/Addresses` |
| ویرایش نشانی | `PUT /api/Account/Address/Addresses/{addressId}` |
| نشانی پیش‌فرض | `PUT /api/Account/Address/Addresses/{addressId}/Default` |
| حذف نشانی | `DELETE /api/Account/Address/Addresses/{addressId}` |

بدنه افزودن و ویرایش:

```json
{
  "title": "خانه",
  "recipientName": "نام گیرنده",
  "province": "تهران",
  "city": "تهران",
  "postalCode": "1234567890",
  "addressLine": "نشانی کامل",
  "mobileNumber": "09123456789",
  "isDefault": true
}
```

## فلو ثبت سفارش

### ۱. نگهداری شناسه نشست و سبد

برای هر مرورگر/نشست یک UUID بسازید و آن را در storage پایدار فرانت نگه دارید. اگر سبدی برای آن نشست وجود نداشت:

`POST /api/ShoppingCart`

```json
{
  "sessionId": "c0a80123-4567-489a-9abc-123456789abc"
}
```

`data` پاسخ، شناسه عددی سبد (`cartId`) است. اگر سبد از قبل وجود دارد، از این مسیر استفاده کنید:

`GET /api/ShoppingCart/session/{sessionId}`

سبد فعلی فقط شناسه نشست و کاربر را نگه می‌دارد؛ اقلام سفارش با endpointهای سفارش ثبت می‌شوند، نه در خود سبد.

### ۲. ساخت سفارش

`POST /api/Order`

```json
{
  "shoppingCartId": 10,
  "shippingAddress": null,
  "mobileNumber": "09123456789"
}
```

`shoppingCartId` الزامی است؛ `shippingAddress` و `mobileNumber` اختیاری‌اند. اگر موبایل ارسال شود باید با `09` و ۱۱ رقم باشد. مقدار `data` پاسخ، `orderId` است. اگر برای همین سبد سفارش در انتظار پرداخت وجود داشته باشد، همان شناسه قبلی برمی‌گردد.

### ۳. افزودن کالاها به سفارش

برای هر کالا:

`POST /api/Order/{orderId}/items`

```json
{
  "productId": 25,
  "quantity": 2
}
```

قیمت را از فرانت ارسال نکنید؛ سرور قیمت محصول را از کاتالوگ می‌خواند. اگر همان محصول دوباره اضافه شود، تعداد قبلی با تعداد جدید جمع می‌شود. تعداد باید بیشتر از صفر باشد.

برای تغییر تعداد:

`PUT /api/Order/{orderId}/items`

```json
{
  "id": 40,
  "quantity": 3
}
```

برای حذف قلم:

`DELETE /api/Order/items/{itemId}`

### ۴. انتخاب نشانی ارسال

ابتدا نشانی‌های کاربر را از endpoint نشانی‌ها دریافت کنید. سپس:

`PUT /api/Order/{orderId}/shipping-address`

با Bearer token و بدنه:

```json
{
  "addressId": 7
}
```

سرور بررسی می‌کند سفارش و نشانی متعلق به کاربر واردشده باشند. سفارش باید در وضعیت `PendingPayment` باشد.

### ۵. نمایش خلاصه سفارش

`GET /api/Order/{orderId}`

در پاسخ `data`، سفارش و `orderItems` قرار می‌گیرند. مبلغ نهایی را از پاسخ سرور نمایش دهید؛ از قیمت یا جمع محاسبه‌شده در مرورگر به‌عنوان مبلغ قطعی استفاده نکنید.

### ۶. شروع پرداخت

`POST /api/Order/{orderId}/pay`

```json
{
  "successReturnUrl": "https://frontend.example.com/payment/result",
  "failedReturnUrl": "https://frontend.example.com/payment/result"
}
```

پاسخ موفق:

```json
{
  "data": {
    "gatewayUrl": "https://...",
    "orderTransactionId": 55
  },
  "message": null,
  "errors": [],
  "statusCode": 200
}
```

مرورگر را به `data.gatewayUrl` هدایت کنید. درگاه پس از پرداخت به callback بک‌اند برمی‌گردد؛ فرانت نباید callback را مستقیماً فراخوانی کند. بک‌اند پس از بررسی پرداخت، کاربر را به یکی از URLهای ارسالی برمی‌گرداند و پارامتر `paymentStatus=success` یا `paymentStatus=failed` را اضافه می‌کند. پس از بازگشت، صفحه نتیجه را تازه‌سازی و وضعیت سفارش را دوباره از API بخواند.

وضعیت‌های سفارش در پاسخ فعلی به‌صورت عددی هستند:

| عدد | وضعیت |
|---:|---|
| 1 | `PendingPayment` |
| 2 | `Paid` |
| 3 | `Cancelled` |
| 4 | `Processing` |
| 5 | `Shipped` |
| 6 | `Completed` |

## محدودیت‌های فعلی API برای پیاده‌سازی پنل

- Handler ثبت‌نام در حال حاضر نقش `User` را به کاربر تازه‌ثبت‌نام‌شده اختصاص نمی‌دهد؛ بنابراین ممکن است `roles` خالی باشد.
- `GET /api/Order/user/{userId}` یک سفارشِ در انتظار پرداخت برمی‌گرداند، نه فهرست تاریخچه سفارش‌ها. مسیر دریافت سفارش با شناسه نیز در حال حاضر فقط سفارش‌های در انتظار پرداخت را برمی‌گرداند؛ تاریخچه سفارش‌های پرداخت‌شده برای پنل کاربر API کامل ندارد.
- در کد فعلی، برخی مسیرهای سبد و سفارش از جمله ساخت سبد/سفارش، افزودن قلم و شروع پرداخت، احراز هویت یا بررسی مالکیت سفارش ندارند. `userId` ارسالی در بدنه سبد را مرز امنیتی در نظر نگیرید. این مسیرها برای استفاده امن چندکاربره نیازمند محدودسازی سمت بک‌اند هستند.
- مسیرهای مدیریت نشانی و انتخاب نشانی سفارش احراز هویت دارند.

## منابع پیاده‌سازی

- [Identity endpoints](./src/Modules/Identity/Identity.Presentation/Endpoints)
- [Order endpoints](./src/Modules/Order/Modules.Order.Presentation/Endpoints)
- [Order DTOs](./src/Modules/Order/Modules.Order.Application.Contract/DTOs)
- [Payment callback](./src/Modules/Payment/Modules.Payment.Presentation/Endpoints/Payments/Read/GetPaymentByIdEndpoint.cs)
