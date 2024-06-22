// export interface User
// {
//     id: string;
//     name: string;
//     email: string;
//     avatar?: string;
//     status?: string;
// }

export interface UserRoleDTO {
    userId: number;
    userName: string;
    roleId: number;
    roleName: string;
}

export interface UserDTO {
    id: number;
    userName: string;
    dateOfBirth: string;
    email: string;
    fullName: string;
    gender: string;
    lastActive: string;
    phoneNumber: string;
    userRoles: UserRoleDTO[];
}

export interface UserOutputDTO {
    user: UserDTO;
    token: string;
}
// export interface User {
//     userName:  string;
//     token: string;
//     photoUrl: string;
//     knownAs: string;
//     gender: string;
//     roles: string[];
// }