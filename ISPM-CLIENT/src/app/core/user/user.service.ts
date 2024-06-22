import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UserDTO, UserOutputDTO } from 'app/core/user/user.types';
import { map, Observable, ReplaySubject, tap } from 'rxjs';

@Injectable({providedIn: 'root'})
export class UserService
{
    private _httpClient = inject(HttpClient);
    private _user: ReplaySubject<UserDTO> = new ReplaySubject<UserDTO>(1);

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for user
     *
     * @param value
     */
    set user(value: UserDTO)
    {
        // Store the value
        this._user.next(value);
    }

    get user$(): Observable<UserDTO>
    {
        return this._user.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get the current signed-in user data
     */
    get(): Observable<UserOutputDTO>
    {
        return this._httpClient.get<UserOutputDTO>('api/common/user').pipe(
            tap((userOutput) =>
            {
                this._user.next(userOutput.user);
            }),
        );
    }

    /**
     * Update the user
     *
     * @param user
     */
    update(user: UserDTO): Observable<UserOutputDTO>
    {
        return this._httpClient.patch<UserOutputDTO>('api/common/user', {user}).pipe(
            map((response) =>
            {
                this._user.next(response.user);
                return response;
            }),
        );
    }
}
