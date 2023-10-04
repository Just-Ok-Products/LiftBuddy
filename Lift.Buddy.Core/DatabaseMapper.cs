using Lift.Buddy.Core.Database.Entities;
using Lift.Buddy.Core.Models;

namespace Lift.Buddy.Core;

public interface IDatabaseMapper
{
    ExerciseDTO Map(Exercise exercise);
    Exercise Map(ExerciseDTO exercise);

    PersonalRecordDTO Map(PersonalRecord personalRecord);
    PersonalRecord Map(PersonalRecordDTO personalRecord);

    UserDTO Map(User user);
    User Map(UserDTO user);

    SecurityQuestionDTO Map(SecurityQuestion securityQuestion);
    SecurityQuestion Map(SecurityQuestionDTO securityQuestion);

    WorkoutPlanDTO Map(WorkoutPlan workoutPlan);
    WorkoutPlan Map(WorkoutPlanDTO workoutPlan);

    WorkoutDayDTO Map(WorkoutDay workoutDay);
    WorkoutDay Map(WorkoutDayDTO workoutDay);
}

public class DatabaseMapper : IDatabaseMapper
{
    public ExerciseDTO Map(Exercise exercise)
    {
        return new ExerciseDTO
        {
            Id = exercise.ExerciseId,
            Name = exercise.Name,
            Series = exercise.Series,
            Repetitions = exercise.Repetitions,
            Time = exercise.Time,
            Rest = exercise.Rest
        };
    }

    public Exercise Map(ExerciseDTO exercise)
    {
        return new Exercise
        {
            ExerciseId = exercise.Id ?? Guid.NewGuid(),
            Name = exercise.Name,
            Series = exercise.Series,
            Repetitions = exercise.Repetitions,
            Time = exercise.Time,
            Rest = exercise.Rest
        };
    }

    public PersonalRecordDTO Map(PersonalRecord personalRecord)
    {
        var record = new PersonalRecordDTO
        {
            Id = personalRecord.PersonalRecordId,
            ExerciseName = personalRecord.ExerciseName,
            Series = personalRecord.Series,
            Reps = personalRecord.Repetitions,
            ExerciseType = personalRecord.ExerciseType,
            UserId = personalRecord.UserId,
            UnitOfMeasure = (UnitOfMeasure)personalRecord.UOM,
            Weight = personalRecord.Weight
        };

        // if (personalRecord.Weight.HasValue && personalRecord.UOM.HasValue)
        // {
        //     var amount = personalRecord.Weight.Value;
        //     var uom = (UnitOfMeasure)personalRecord.UOM.Value;
        //     record.Weight = new Weight(amount, uom);
        // }

        return record;
    }

    public PersonalRecord Map(PersonalRecordDTO personalRecord)
    {
        return new PersonalRecord
        {
            PersonalRecordId = personalRecord.Id ?? Guid.NewGuid(),
            ExerciseName = personalRecord.ExerciseName,
            Series = personalRecord.Series,
            Repetitions = personalRecord.Reps,
            Weight = personalRecord.Weight,
            UOM = (int?)personalRecord.UnitOfMeasure,
            ExerciseType = personalRecord.ExerciseType,
            UserId = personalRecord.UserId
        };
    }

    public UserDTO Map(User user)
    {
        return new UserDTO
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Credentials = new Credentials { Username = user.Username },
        };
    }

    public User Map(UserDTO user)
    {
        return new User
        {
            Username = user.Credentials.Username,
            Password = Utils.HashString(user.Credentials.Password),
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            SecurityQuestions = user.SecurityQuestions.Select(q =>
            {
                return new SecurityQuestion
                {
                    SecurityQuestionId = Guid.NewGuid(),
                    Answer = q.Answer,
                    Question = q.Question
                };
            }).ToArray()
        };
    }

    public SecurityQuestionDTO Map(SecurityQuestion securityQuestion)
    {
        throw new NotImplementedException();
    }

    public SecurityQuestion Map(SecurityQuestionDTO securityQuestion)
    {
        throw new NotImplementedException();
    }

    public WorkoutPlanDTO Map(WorkoutPlan workoutPlan)
    {
        return new WorkoutPlanDTO
        {
            Id = workoutPlan.WorkoutPlanId,
            Name = workoutPlan.Name,
            CreatorId = workoutPlan.CreatorId,
            ReviewAverage = workoutPlan.ReviewAverage,
            ReviewsCount = workoutPlan.ReviewCount,
            WorkoutDays = workoutPlan.WorkoutDays.Select(d => Map(d))
        };
    }

    // ext.CreatedBy(u.id);
    public WorkoutPlan Map(WorkoutPlanDTO workoutPlan)
    {
        return new WorkoutPlan
        {
            WorkoutPlanId = workoutPlan.Id ?? Guid.NewGuid(),
            Name = workoutPlan.Name,
            ReviewAverage = workoutPlan.ReviewAverage,
            ReviewCount = workoutPlan.ReviewsCount,
            WorkoutDays = workoutPlan.WorkoutDays.Select(d => Map(d)).ToArray(),
            CreatorId = workoutPlan.CreatorId,
        };
    }

    public WorkoutDayDTO Map(WorkoutDay workoutDay)
    {
        return new WorkoutDayDTO
        {
            Id = workoutDay.Id,
            Day = workoutDay.Day,
            Exercises = workoutDay.Exercises.Select(e => Map(e))
        };
    }

    public WorkoutDay Map(WorkoutDayDTO workoutDay)
    {
        return new WorkoutDay
        {
            Id = workoutDay.Id ?? Guid.NewGuid(),
            Day = workoutDay.Day,
            Exercises = workoutDay.Exercises.Select(e => Map(e)).ToArray(),
        };
    }
}