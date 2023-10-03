import { WorkoutDay } from "./WorkoutDay";

export class WorkoutPlan {
    public id: string = '';
    public name: string = '';
    public createdBy: string = '';
    public workoutDays: WorkoutDay[] = [];
    public reviewAverage: number = 0;
    public reviewCount: number = 0;
}
