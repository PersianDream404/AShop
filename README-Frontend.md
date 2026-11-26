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

### پروفایل کاربر

هر دو endpoint نیازمند Bearer token هستند و فقط به پروفایل کاربر واردشده دسترسی دارند:

| عملیات | روش و مسیر |
|---|---|
| دریافت پروفایل من | `GET /api/Account/Profile` |
| ویرایش پروفایل من | `PUT /api/Account/Profile` |

ویرایش نام و نام‌خانوادگی:

```json
{
  "firstName": "نام",
  "lastName": "نام‌خانوادگی"
}
```

پاسخ شامل `id`، `firstName`، `lastName` و `mobile` است. شماره موبایل فعلاً فقط خواندنی است.

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

### اتصال سبد نشست به حساب کاربر

پس از ورود کاربر، برای ادغام سفارش باز سبد نشست با سبد قبلی حساب، شناسه سبد نشست را ارسال کنید:

`PUT /api/ShoppingCart/{cartId}/link-user`

این مسیر به Bearer token نیاز دارد و بدنه ندارد؛ شناسه کاربر فقط از توکن خوانده می‌شود. در صورت وجود سبد قبلی کاربر، اقلام سفارش‌های در انتظار پرداخت ادغام می‌شوند و سفارش‌های قبلی دیگر حفظ و به سبد حساب منتقل می‌شوند. سبد نشست دیگر حذف نخواهد شد مگر پس از انتقال سفارش‌های آن.

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

### ۵. مشاهده سفارش‌های من و جزئیات سفارش

این مسیرها نیازمند Bearer token هستند و فقط سفارش‌های کاربر واردشده را برمی‌گردانند؛ نیازی به ارسال `userId` نیست.

فهرست سفارش‌ها صفحه‌بندی‌شده است:

`GET /api/Order/user?pageNumber=1&pageSize=10`

`pageNumber` از ۱ شروع می‌شود و `pageSize` بین ۱ تا ۱۰۰ است (پیش‌فرض: صفحه ۱ با ۱۰ سفارش). پاسخ در `data.list` شامل خلاصه سفارش‌ها (`id`، `totalAmount`، `status`، `createdAt` و `itemCount`) و در `data.pagination` شامل اطلاعات صفحه‌بندی است. سفارش‌ها از جدید به قدیم مرتب می‌شوند.

برای جزئیات یک سفارش:

`GET /api/Order/{orderId}`

این مسیر فقط سفارش متعلق به کاربر واردشده را برمی‌گرداند و برای همه وضعیت‌های سفارش قابل استفاده است. در پاسخ `data`، جزئیات سفارش و `orderItems` قرار می‌گیرند. اگر سفارش وجود نداشته باشد یا متعلق به کاربر دیگری باشد، پاسخ «سفارش یافت نشد» برمی‌گردد. مبلغ نهایی را از پاسخ سرور نمایش دهید؛ از قیمت یا جمع محاسبه‌شده در مرورگر به‌عنوان مبلغ قطعی استفاده نکنید.

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
- دریافت فهرست و جزئیات سفارش از مسیرهای `/api/Order/user` و `/api/Order/{orderId}` فقط برای کاربر واردشده فعال است؛ مسیر قبلی `/api/Order/user/{userId}` دیگر استفاده نمی‌شود.
- مسیر اتصال سبد نشست به حساب کاربر نیازمند Bearer token است و کاربر از توکن تعیین می‌شود؛ `userId` ارسالی در بدنه مبنای مالکیت نیست.
- برخی مسیرهای دیگر سبد و سفارش، از جمله ساخت سبد/سفارش، افزودن قلم و شروع پرداخت، هنوز احراز هویت یا بررسی مالکیت کامل ندارند و برای استفاده امن چندکاربره نیازمند محدودسازی سمت بک‌اند هستند.
- مسیرهای مدیریت نشانی و انتخاب نشانی سفارش احراز هویت دارند.

## منابع پیاده‌سازی

- [Identity endpoints](./src/Modules/Identity/Identity.Presentation/Endpoints)
- [Order endpoints](./src/Modules/Order/Modules.Order.Presentation/Endpoints)
- [Order DTOs](./src/Modules/Order/Modules.Order.Application.Contract/DTOs)
- [Payment callback](./src/Modules/Payment/Modules.Payment.Presentation/Endpoints/Payments/Read/GetPaymentByIdEndpoint.cs)
