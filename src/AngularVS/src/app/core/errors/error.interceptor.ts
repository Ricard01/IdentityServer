import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse} from '@angular/common/http';
import { EMPTY, Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';

// https://medium.com/calyx/global-error-handling-with-angular-and-ngrx-d895f7df2895
// https://stackoverflow.com/questions/46433953/how-to-cancel-current-request-in-interceptor-angular-4
// https://stackoverflow.com/questions/54765185/global-errorhandler-is-not-working-after-implementing-catcherror-in-ngrx-effect?noredirect=1&lq=1

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

        return next.handle(request).pipe(
            catchError(error => {

                let handledError: boolean = false;
                console.error(error.message +'Error Ricardo' + JSON.stringify(error) ) // Delete in Produccition
                if (error instanceof HttpErrorResponse) {
                  if (error.status == 0)
                  {
                    console.log('error 0')
                    handledError = true;
                    this.router.navigate(['error' , {error : error.status}], {skipLocationChange: true});

                    console.error(error.message ) // Delete in Produccition
                  }

                     if(error.status == 403 )
                        {
                            handledError = true;
                            this.router.navigate(['error/404' , {error : error.status}], {skipLocationChange: true});

                            console.error(error.message ) // Delete in Produccition
                        }
                  }


                if (handledError) {

                    return EMPTY // Cut the interceptor chain

                } else {

                     return throwError(error);  // need to rethrow so redux and handler can catch the error but only if != 403
                }

        }));

    }
}
