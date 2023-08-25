import { UserData } from "./UserData";

export class RegistrationCredentials extends UserData {
  public password: string | null = null;
  public isAdmin: boolean = false;
  public questions: string[] = [];
  public answers: string[] = [];
}
