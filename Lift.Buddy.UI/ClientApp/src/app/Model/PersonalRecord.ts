import { ExercizeType } from "./Enums/ExercizeType";
import { UnitOfMeasures } from "./Enums/UnitOfMeasures";

export class PersonalRecord {
  public exerciseName: string = '';
  public series: number = 0;
  public reps: number = 0;
  public weight: number = 0;
  public unitOfMeasure: string = UnitOfMeasures.KG;
  public exercizeTipe: ExercizeType = ExercizeType.Weight;
}
