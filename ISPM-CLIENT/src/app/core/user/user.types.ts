export interface User
{
    id: string;
    name: string;
    email: string;
    avatar?: string;
    status?: string;
}



export interface User {
    userName:  string;
    token: string;
    photoUrl: string;
    knownAs: string;
    gender: string;
    roles: string[];
}