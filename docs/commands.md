# Commands
## Commitizen
`cz --name cz_commitizen_emoji commit`
## EntityFramework
Run from `eShop/Infrastructure` folder
### Add migration
`dotnet ef migrations add <name> -s ..\Web\Api\ -o .\Data\Migrations`
### Update database
`dotnet ef database update -s ..\Web\Api\`