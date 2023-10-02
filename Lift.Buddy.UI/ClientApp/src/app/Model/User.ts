import { Credentials } from "./Credentials";
import { SecurityQuestion } from "./SecurityQuestions";

export class User {
    public id: string = ""
    public username: string = "";
    public name: string = "";
    public surname: string = "";
    public email: string = "";
    public credentials: Credentials = new Credentials("", "");
    public securityQuestions: SecurityQuestion[] = []
}
