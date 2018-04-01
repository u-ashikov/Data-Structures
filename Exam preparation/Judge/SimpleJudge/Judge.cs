using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Judge : IJudge
{
    private OrderedSet<int> users;

    private OrderedSet<int> contests;

    private Dictionary<int,Submission> submissions;

    public Judge()
    {
        this.users = new OrderedSet<int>((x,y) => x.CompareTo(y));
        this.contests = new OrderedSet<int>((x,y) => x.CompareTo(y));
        this.submissions = new Dictionary<int, Submission>();
    }

    public void AddSubmission(Submission submission)
    {
        if (!this.users.Contains(submission.UserId) || !this.contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        if (!this.submissions.ContainsKey(submission.Id))
        {
            this.submissions.Add(submission.Id, submission);
        }
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.submissions.Values.OrderBy(s=>s);
    }

    public void AddUser(int userId)
    {
        this.users.Add(userId);
    }

    public IEnumerable<int> GetUsers() => this.users;

    public void AddContest(int contestId)
    {
        this.contests.Add(contestId);
    }

    public IEnumerable<int> GetContests() => this.contests;

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }

        this.submissions.Remove(submissionId);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.submissions.Values.Where(s => s.Points >= minPoints && s.Points <= maxPoints && s.Type == submissionType).ToList();
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return this.submissions.Values.Where(s => s.UserId == userId)
            .GroupBy(x => x.ContestId).Select(x => x.OrderByDescending(s => s.Points).ThenBy(s => s.Id).First()).OrderByDescending(x => x.Points).ThenBy(x => x.Id).Select(x => x.ContestId);
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        if (!this.submissions.Values.Any(s=>s.ContestId == contestId && s.UserId == userId && s.Points == points))
        {
            throw new InvalidOperationException();
        }

        return this.submissions.Values.Where(s => s.ContestId == contestId && s.UserId == userId && s.Points == points).ToList();
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        if (!this.submissions.Values.Any(s=>s.Type == submissionType))
        {
            return new List<int>();
        }

        return this.submissions.Values.Where(s=>s.Type == submissionType).Select(s=>s.ContestId).Distinct();
    }
}
