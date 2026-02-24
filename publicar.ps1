Write-Host "🚀 Iniciando proceso de respaldo y publicación..." -ForegroundColor Cyan

# --- 1. RESPALDO DEL CÓDIGO FUENTE (Esto arregla tus 14 commits pendientes) ---
Write-Host "📦 Respaldando tu código fuente en VS Code..." -ForegroundColor Yellow
# Nos aseguramos de estar en la carpeta principal
Set-Location -Path $PSScriptRoot 
git add .
git commit -m "Respaldo y actualización de código fuente"
# Forzamos la unión pacífica por si quedó algún rastro del error anterior
git pull origin main --allow-unrelated-histories --no-edit
git push origin main

# --- 2. CONSTRUCCIÓN DE LA APP ---
Write-Host "🔨 Compilando la aplicación Blazor..." -ForegroundColor Yellow
if (Test-Path release) { Remove-Item -Recurse -Force release }
dotnet publish -c Release -o release

# --- 3. PREPARACIÓN WEB ---
Set-Location -Path release/wwwroot
(Get-Content index.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content index.html
(Get-Content 404.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content 404.html
New-Item -ItemType File -Name ".nojekyll" -Force

# --- 4. SUBIDA EXCLUSIVA DE LA PÁGINA WEB ---
Write-Host "🌐 Subiendo la web a Internet (Rama gh-pages)..." -ForegroundColor Yellow
git init
git add .
git commit -m "Actualización automática de la web"
git branch -M gh-pages
git remote add origin https://github.com/Home-Bound/HomeBound.git
# ¡Ojo aquí! Ahora empujamos a gh-pages, dejando tu rama main en paz
git push -u origin gh-pages --force

# Regresar a la carpeta principal para terminar
Set-Location -Path ../../

Write-Host "✅ ¡Listo! Código respaldado y web actualizada." -ForegroundColor Green