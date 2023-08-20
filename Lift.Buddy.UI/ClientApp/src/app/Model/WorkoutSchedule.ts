import { WorkoutDay } from "./WorkoutDay";

export class WorkoutSchedule {
  public id: number | undefined;
  public workoutDays: WorkoutDay[] = [];
}
