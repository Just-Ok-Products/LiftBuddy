import { Credentials } from "./Credentials";

export class User {
  public username: string = "";
  public name: string = "";
  public surname: string = "";
  public email: string = "";
  public credentials : Credentials = new Credentials();
}
