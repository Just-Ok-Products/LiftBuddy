import { WorkoutDay } from "./WorkoutDay";

export class WorkoutSchedule {
  public id: number | undefined;
  public name: string = '';
  public workoutDays: WorkoutDay[] = [];
}
