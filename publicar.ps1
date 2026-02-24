Write-Host "🚀 Iniciando despliegue a GitHub..." -ForegroundColor Cyan

# 1. Limpieza
if (Test-Path release) { Remove-Item -Recurse -Force release }

# 2. Construir la App
dotnet publish -c Release -o release

# 3. Entrar a la carpeta (Advertencia de alias solucionada)
Set-Location -Path release/wwwroot

# 4. Cambiar el href para internet en index y en 404
(Get-Content index.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content index.html
(Get-Content 404.html) -replace 'base href="/"', 'base href="/HomeBound/"' | Set-Content 404.html

# 5. Crear archivos vitales
New-Item -ItemType File -Name ".nojekyll" -Force
# (Línea de copia del 404 eliminada para proteger nuestro script de redirección)

# 6. Subir a GitHub
git init
git add .
git commit -m "Actualizacion automatica"
git branch -M main
git remote add origin https://github.com/Home-Bound/HomeBound.git
git push -u origin main --force

Write-Host "✅ ¡Listo! Tu sitio se actualizará en 2 minutos." -ForegroundColor Green