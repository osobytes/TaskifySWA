export interface LoggedInUser{
    clientPrincipal?: ClientPrincipal;
}

export interface ClientPrincipal{
    identityProvider: string;
    userId: string;
    userDetails: string;
    userRoles: string[];
}