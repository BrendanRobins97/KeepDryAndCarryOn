using UnityEngine;

static class WeightedProbability
{
    public static int TotalWeights<T, U>(T[] relativeProbabilities) where T : IWeighted<U>
    {
        Debug.Assert(relativeProbabilities != null && relativeProbabilities.Length > 0,
            "Parameter relativeProbabilities is null or empty");

        int total = 0;

        foreach (T weighted in relativeProbabilities)
        {
            total += weighted.Weight;
        }

        return total;
    }

    public static int RandomIndex<T, U>(T[] relativeProbabilities, int totalRelativeProbability) where T : IWeighted<U>
    {
        Debug.Assert(relativeProbabilities != null && relativeProbabilities.Length > 0,
            "Parameter relativeProbabilities is null or empty");
        Debug.Assert(totalRelativeProbability >= 0,
            "Parameter totalRelativeProbability is negative");

        int num = Random.Range(1, totalRelativeProbability + 1);
        int total = 0;

        for (int i = 0; i < relativeProbabilities.Length; ++i)
        {
            if (total >= num)
                return i - 1;

            total += relativeProbabilities[i].Weight;
        }

        return relativeProbabilities.Length - 1;
    }
}
