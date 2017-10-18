export class LoggedInUser {
    constructor(access_token: string, avatar: string, email: string, fullName: string, role: string, username: string) {
        this.access_token = access_token;
        this.avatar = avatar;  
        this.email = email;                
        this.fullName = fullName;     
        this.role = role;        
        this.username = username;
    }
    public id: string;
    public access_token: string;
    public avatar: string;
    public email: string;
    public fullName: string;
    public role : string;
    public username: string;
    
}