import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor() {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error instanceof HttpErrorResponse) {

                    const applicationError = error.headers.get('Application-Error');
                    if (applicationError) {
                        return throwError(applicationError);
                    }

                    const serverError = error.error;

                    let modelStateErrors = '';
                    if (serverError && typeof serverError === 'object') {
                        const keys = Object.keys(serverError);
                        for (const key of keys) {
                            // ex: password validation on api has description error, regular model state errors do not
                            if (serverError[key].description) {
                                modelStateErrors += serverError[key].description + '\n';
                            } else if (serverError[key]) {
                                modelStateErrors += serverError[key] + '\n';
                            }
                        }
                    }

                    return throwError(modelStateErrors.length && modelStateErrors || serverError || 'Server Error');
                }
            })
        );
    }

}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
