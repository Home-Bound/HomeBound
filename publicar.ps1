Write-Host "🚀 Iniciando proceso de respaldo y publicación..." -ForegroundColor Cyan

# 1. Asegurar código fuente en main
Write-Host "📦 Respaldando tu código fuente en VS Code..." -ForegroundColor Yellow
Set-Location -Path $PSScriptRoot 
git add .
git commit -m "Respaldo antes de publicar V2"
git push origin main

# 2. Compilar
Write-Host "🔨 Compilando la aplicación Blazor..." -ForegroundColor Yellow
if (Test-Path release) { Remove-Item -Recurse -Force release }
dotnet publish -c Release -o release

# 3. Preparar la web
Set-Location -Path release/wwwroot
(Get-Content index.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content index.html
(Get-Content 404.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content 404.html
New-Item -ItemType File -Name ".nojekyll" -Force

# 4. Subir a gh-pages
Write-Host "🌐 Subiendo la web a Internet (Rama gh-pages)..." -ForegroundColor Yellow
git init
git add .
git commit -m "Despliegue de la Fase 2 (Seguimiento)"
git branch -M gh-pages
git remote add origin https://github.com/Home-Bound/HomeBound.git
git push -u origin gh-pages --force

Set-Location -Path ../../
Write-Host "✅ ¡Listo! Código respaldado y web actualizada." -ForegroundColor Green