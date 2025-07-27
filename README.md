# Deskstones.LMS.WebAPI
supports a learning management platform which would be used by an e-learning company

To update migrations use :
Add-Migration InitialCreate -Project Deskstones.LMS.Infrastructure -StartupProject Deskstones.LMS.WebAPI
Update-Database -Project Deskstones.LMS.Infrastructure -StartupProject Deskstones.LMS.WebAPI


dotnet ef migrations add updateTableCols -p Deskstones.LMS.Infrastructure -s Deskstones.LMS.WebAPI

dotnet ef database update -p Deskstones.LMS.Infrastructure -s Deskstones.LMS.WebAPI
