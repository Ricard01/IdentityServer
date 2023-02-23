# Primeros Pasos

1. Modificar package.json

    ```Typescript
        "start": "ng serve --port 5002 --ssl"
    ```

2. Agregar libreria angular-auth-oidc-client

    ```Typescript
        ng add angular-auth-oidc-client
    ```
  
3. Modificar tsconfig.json

    ```Typescript
        "baseUrl": "src"
    ```

4. Crear el component callback que se encargara de procesar la authenticacion.

5. Crear carpeta config.
    1. Crear app.config.interface (interface para configurar la libreria de authenticacion).
    2. Crear app.config (injectionToken).
    3. Crear los archivos de configuracion localhost y production con su respectiva informacion.

6. Modificar las variables de entorno para que tomen las configuraciones del paso anterior.

7. Crear un servicio para configurar la authenticacion.

8. Crear un un modulo para terminar de configurar la autenticacion y proveer el servicio.

9. Importar el modulo Auth.Config.Module a AppModule.

10. Agregar el Interceptor de la libreria OIDC

Con esto ya estara listo la aplicacion de angular para trabajar con Identity Server.

## Angular Style

1. Agregar angular material y modificar css al gusto.

Notas

* Cuando pararece que tiene un loop en el inicio de sesion es porque el callback de identityserver tiene algun detalle por ejemplo , en angular no se ha especificado si despues de iniciar sesion ya esta autenticado por lo que vuelve hacer el proceso de authenticacion.
