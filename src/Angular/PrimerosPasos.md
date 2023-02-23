# Primeros Pasos

1. Modificar package.json

    ```Typescript
        "start": "ng serve --port 5003 --ssl"
    ```

2. Agregar libreria angular-auth-oidc-client

    ```Typescript
        ng add angular-auth-oidc-client
    ```

Notas

* Cuando pararece que tiene un loop en el inicio de sesion es porque el callback de identityserver tiene algun detalle por ejemplo , en angular no se ha especificado si despues de iniciar sesion ya esta autenticado por lo que vuelve hacer el proceso de authenticacion.
