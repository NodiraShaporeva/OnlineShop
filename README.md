# OnlineShop
Training project on ASP.NET Core 7
После того, как Вы скачали этот проект, Вам нужно будет провести миграции, введя в терминале следующие команды:
cd .\OnlineShop.WebApi\
Таким образом Вы перейдете в тот проект откуда нужно будет проводить миграции, а потом вводите следующую команду:
dotnet ef migrations add Migrations -p ../OnlineShop.Data
и создаете датабазу:
dotnet ef database update
