# Phishing Simulation — Backend

საბაკალავრო კვლევის პროექტი: ვიშინგის სიმულაცია და მომხმარებელთა ცნობიერება

## პროექტის შესახებ

ASP.NET Core 8 backend API ვიშინგ სიმულაციის პლატფორმისთვის. მართავს credential capture-ს, admin authentication-ს და მონაცემთა ბაზის ოპერაციებს.

## API Endpoints

| Method | Endpoint | აღწერა |
|--------|----------|--------|
| `POST` | `/api/capture` | Credential-ების შენახვა |
| `POST` | `/api/admin/login` | Admin-ის ავთენტიფიკაცია |
| `GET` | `/api/admin/captures` | ყველა capture-ის სია |
| `GET` | `/api/admin/stats` | სტატისტიკა |
| `GET` | `/api/admin/export` | CSV ექსპორტი |

## ტექნოლოგიები

- **Framework:** ASP.NET Core 8
- **Database:** PostgreSQL
- **Auth:** JWT Bearer Token
- **ORM:** Entity Framework Core
- **Deployment:** Railway

## გარემოს ცვლადები

```env
DATABASE_URL=postgresql://user:password@host:port/dbname
JWT_SECRET=your-secret-key
ADMIN_USERNAME=admin
ADMIN_PASSWORD=your-password
FRONTEND_URL=https://techop.vercel.app
```

## გაშვება

```bash
dotnet restore
dotnet run
```

## პროექტის ისტორია

| თარიღი | ცვლილება |
|--------|----------|
| 2026-04-10 | ASP.NET Core 8 პროექტის შექმნა |
| 2026-04-14 | PostgreSQL context და models |
| 2026-04-18 | Credential capture API endpoint |
| 2026-04-23 | Admin auth და JWT middleware |
| 2026-04-28 | Admin dashboard API |
| 2026-05-05 | IP და user-agent logging |
| 2026-05-13 | CORS პოლიტიკის გამოსწორება |
| 2026-05-22 | API response models refactor |
| 2026-06-05 | README და deployment guide |

## ავტორები

- **ლაშა ღონღაძე** — [@lashaghongha1](https://github.com/lashaghongha1)
- **მარიამ ხარებავა** — [@mkharebava](https://github.com/mkharebava)

## Frontend

Frontend repo: [sabak-frontend](https://github.com/lashaghongha1/sabak-frontend)

---
> ეს პროექტი შეიქმნა მხოლოდ საგანმანათლებლო და კვლევითი მიზნებისთვის.
