export interface ICurrentUser{
    result:
    {
        succeeded:boolean,
        isLockedOut:boolean,
        isNotAllowed:boolean,
        requiresTwoFactor:boolean
    },
    userName:string,
    email:string,
    token:string
}