export class RegistrationCredentials {
  public username: string | null = null;
  public name: string | null = null;
  public surname: string | null = null;
  public email: string | null = null;
  public password: string | null = null;
  public isAdmin: boolean = false;
  public questions: string[] = [];
  public answers: string[] = [];
}
