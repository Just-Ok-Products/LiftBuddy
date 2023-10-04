import { ExerciseType } from "./Enums/ExerciseType";
import { UnitOfMeasure } from "./Enums/UnitOfMeasures";

export class PersonalRecord {
    public exerciseName: string = '';
    public series: number = 0;
    public reps: number = 0;
    public weight: number = 0;
    public unitOfMeasure: string = UnitOfMeasure.KG;
    public exerciseType: ExerciseType = ExerciseType.Weight;
    public id: string | null = null;
    public userId: string | null = null;
}
