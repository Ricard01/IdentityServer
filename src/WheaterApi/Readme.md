# Orden de Creación y Modificación desde Ceros 

1. Para poder validar que la Api esta protegida primero es necesario Agregar [Authorize] al controlador WeatherForecast.

2. Agregar la libreria Jwt.
	> Microsoft.AspNetCore.Authentication.JwtBearer

3. En Program agregar los servicios de Authenticacion.

4. En la terminal de Comandos.

	1. Solicitar el Token 
	```
	curl -X POST -H "content-type: application/x-www-form-urlencoded" -H "Cache-Control: no-cache" -d 
	"client_id=m2m.client&scope=weatherapi.read&client_secret=SuperSecretPassword&grant_type=client_credentials" 
	"https://localhost:5000/connect/token"
    ```

	2. Obtener la informacion del contralador usando el token.
	
	```
	curl -X GET -H "Authorization: Bearer eyJhbGci...b1XA" -H "cache-control: no-cache" "https://localhost:5001/weatherforecast"
	```

	Con esto validamos la Api esta correctamente protediga con Identity Server.
