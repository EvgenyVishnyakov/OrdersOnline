<h1>🛒 OnlineOrderWebApp</h1>

<p>Веб-приложение для оформления онлайн-заказов, разработанное с использованием ASP.NET MVC.</p>

<hr />

<h2>📁 Структура проекта</h2>

<p>Репозиторий представляет собой веб-приложение для онлайн-заказов. Основные компоненты проекта:</p>

<ul>
  <li><code>OnlineOrderWebApp/</code> — основной каталог проекта.</li>
  <li><code>OnlineOrderWebApp/Controllers/</code> — контроллеры ASP.NET MVC.</li>
  <li><code>OnlineOrderWebApp/Models/</code> — модели данных.</li>
  <li><code>OnlineOrderWebApp/OnlineOrderWebApp.csproj</code> — файл проекта.</li>
</ul>

<hr />

<h2>⚙️ Предварительные требования</h2>

<p>Перед запуском убедитесь, что у вас установлены:</p>

<ul>
  <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">.NET SDK 6.0 или новее</a></li>
  <li><a href="https://visualstudio.microsoft.com/vs/">Visual Studio 2022</a> с поддержкой ASP.NET и веб-разработки</li>
  <li><a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads">SQL Server</a> (если используется база данных)</li>
</ul>

<hr />

<h2>🚀 Инструкция по запуску сервиса</h2>

<h3>1. Клонируйте репозиторий</h3>

<pre><code>git clone https://github.com/EvgenyVishnyakov/OnlineOrderWebApp.git
cd OnlineOrderWebApp
</code></pre>

<h3>2. Откройте проект в Visual Studio</h3>

<ul>
  <li>Запустите Visual Studio.</li>
  <li>Выберите <strong>"Открыть проект или решение"</strong>.</li>
  <li>Укажите путь к файлу <code>OnlineOrderWebApp.csproj</code>.</li>
</ul>

<h3>3. Настройте строку подключения к базе данных</h3>

<ul>
  <li>Откройте файл <code>appsettings.json</code>.</li>
  <li>Найдите раздел <code>ConnectionStrings</code>.</li>
  <li>Укажите параметры подключения к вашей базе данных SQL Server, например:</li>
</ul>

<pre><code>{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OnlineOrderDb;Trusted_Connection=True;"
  }
}
</code></pre>

<h3>4. Примените миграции базы данных (если используются)</h3>

<ul>
  <li>Откройте <strong>Консоль диспетчера пакетов</strong> в Visual Studio и выполните команду:</li>
</ul>

<pre><code>Update-Database
</code></pre>

<h3>5. Запустите приложение</h3>

<ul>
  <li>Нажмите <code>F5</code> или выберите <strong>"Запуск без отладки"</strong> в Visual Studio.</li>
  <li>Приложение откроется в браузере по адресу:</li>
</ul>

<pre><code>https://localhost:5001
или
http://localhost:5000
</code></pre>

<hr />

<h2>✅ Проверка работы</h2>

<p>После запуска вы должны увидеть главную страницу приложения. Убедитесь, что все функции работают корректно, включая:</p>

<ul>
  <li>Методы API</li>
</ul>

<hr />

<h2>📬 Обратная связь</h2>

<p>Если вы нашли ошибку или хотите предложить улучшение, создайте <a href="https://github.com/EvgenyVishnyakov/OnlineOrderWebApp/issues">issue</a> или отправьте pull request.</p>

<hr />
