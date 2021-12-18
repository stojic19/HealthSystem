export interface IMaliciousPatient {
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
}
export class MaliciousPatient {
    constructor(
    public userName: string,
    public firstName: string,
    public lastName: string,
    public email: string){}
}