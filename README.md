# ProductCatalogApi

ASP.NET Core Web API для каталога продуктов. Проект позволяет создать продукт и прикрепить к нему несколько изображений одним `multipart/form-data` запросом.

## Возможности

- создание продукта с названием, описанием и ценой;
- загрузка до 10 фотографий продукта;
- поддерживаемые форматы: `.jpg`, `.jpeg`, `.png`, `.webp`;
- максимальный размер одного изображения — 5 МБ;
- хранение файлов в `wwwroot/uploads/products`;
- получение продукта вместе со списком ссылок на фотографии.

## API

### Создание продукта

`POST /api/product`

Content-Type: `multipart/form-data`

Поля формы:

- `name` — обязательное название;
- `description` — описание;
- `price` — цена;
- `photos` — одно или несколько изображений. Поле можно повторять.

Пример через curl:

```bash
curl -X POST "https://localhost:7162/api/product" \
  -F "name=Notebook" \
  -F "description=Portable computer" \
  -F "price=999.99" \
  -F "photos=@photo-front.jpg" \
  -F "photos=@photo-back.jpg"
```

### Получение продукта

`GET /api/product/{id}`

Ответ содержит массив `photos` с URL сохранённых изображений.

## Запуск

1. Укажите строку подключения PostgreSQL в `appsettings.json`.
2. Создайте/обновите схему базы данных миграцией EF Core:
   `dotnet ef migrations add AddProductsAndPhotos` и `dotnet ef database update`.
3. Запустите проект: `dotnet run --project ProductCatalogApi.csproj`.
4. Откройте Swagger по адресу `https://localhost:7162/swagger`.

## Скриншоты

Добавьте сюда скриншоты Swagger после запуска и выполнения запроса:

- `docs/screenshots/swagger-create-product.png` — форма загрузки нескольких фотографий;
- `docs/screenshots/swagger-product-response.png` — ответ с массивом ссылок на фото.

## Репозиторий

Ссылка на репозиторий: **добавьте фактический URL GitHub после публикации проекта**.

Например: `https://github.com/<ваш-профиль>/ProductCatalogApi`
