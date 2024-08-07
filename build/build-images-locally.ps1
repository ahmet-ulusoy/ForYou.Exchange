param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../"
### Apps Folders
$mvcAppFolder = Join-Path $slnFolder "apps/web/src/ForYou.Exchange.Web"
$authserverFolder = Join-Path $slnFolder "apps/auth-server/src/ForYou.Exchange.AuthServer"
$publicWebFolder = Join-Path $slnFolder "apps/public-web/src/ForYou.Exchange.PublicWeb"

### Microservice Folders
$identityServiceFolder = Join-Path $slnFolder "services/identity/src/ForYou.Exchange.IdentityService.HttpApi.Host"
$administrationServiceFolder = Join-Path $slnFolder "services/administration/src/ForYou.Exchange.AdministrationService.HttpApi.Host"
$saasServiceFolder = Join-Path $slnFolder "services/saas/src/ForYou.Exchange.SaasService.HttpApi.Host"
$productServiceFolder = Join-Path $slnFolder "services/product/src/ForYou.Exchange.ProductService.HttpApi.Host"

### Gateway Folders
$webGatewayFolder = Join-Path $slnFolder "gateways/web/src/ForYou.Exchange.WebGateway"
$webPublicGatewayFolder = Join-Path $slnFolder "gateways/web-public/src/ForYou.Exchange.PublicWebGateway"

### DB Migrator Folder
$dbmigratorFolder = Join-Path $slnFolder "shared/ForYou.Exchange.DbMigrator"

Write-Host "*** BUILDING DB MIGRATOR ***" -ForegroundColor Green
Set-Location $dbmigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-db-migrator:$version .

Write-Host "===== BUILDING MICROSERVICES =====" -ForegroundColor Yellow 
### IDENTITY-SERVICE
Write-Host "*** BUILDING IDENTITY-SERVICE ***" -ForegroundColor Green
Set-Location $identityServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-service-identity:$version .

### ADMINISTRATION-SERVICE
Write-Host "*** BUILDING ADMINISTRATION-SERVICE ***" -ForegroundColor Green
Set-Location $administrationServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-service-administration:$version .

### SAAS-SERVICE
Write-Host "*** BUILDING SAAS-SERVICE ***" -ForegroundColor Green
Set-Location $saasServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-service-saas:$version .

### PRODUCT-SERVICE
Write-Host "*** BUILDING PRODUCT-SERVICE ***" -ForegroundColor Green
Set-Location $productServiceFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-service-product:$version .
Write-Host "===== COMPLETED BUILDING MICROSERVICES =====" -ForegroundColor Yellow 

Write-Host "===== BUILDING GATEWAYS =====" -ForegroundColor Yellow 
### WEB-GATEWAY
Write-Host "*** BUILDING WEB-GATEWAY ***" -ForegroundColor Green
Set-Location $webGatewayFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-gateway-web:$version .

### PUBLICWEB-GATEWAY
Write-Host "*** BUILDING WEB-PUBLIC-GATEWAY ***" -ForegroundColor Green
Set-Location $webPublicGatewayFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-gateway-web-public:$version .
Write-Host "===== COMPLETED BUILDING GATEWAYS =====" -ForegroundColor Yellow 

Write-Host "===== BUILDING APPLICATIONS =====" -ForegroundColor Yellow 
### AUTH-SERVER
Write-Host "*** BUILDING AUTH-SERVER ***" -ForegroundColor Green
Set-Location $authserverFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-app-authserver:$version .

### PUBLIC-WEB
Write-Host "*** BUILDING WEB-PUBLIC ***" -ForegroundColor Green
Set-Location $publicWebFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t foryou/exchange-app-public-web:$version .
### MVC WEB App
if (Test-Path -Path $mvcAppFolder) {
    Write-Host "*** BUILDING MVC WEB APPLICATION ***" -ForegroundColor Green
    Set-Location $mvcAppFolder
    dotnet publish -c Release
    docker build -f Dockerfile.local -t foryou/exchange-app-web:$version .
}
Write-Host "===== COMPLETED BUILDING APPLICATIONS =====" -ForegroundColor Yellow 
### ALL COMPLETED
Write-Host "ALL COMPLETED" -ForegroundColor Green
Set-Location $currentFolder