using UnityEngine;

static class RelativeProbability
{
    public static int Total(int[] relativeProbabilities)
    {
        Debug.Assert(relativeProbabilities != null && relativeProbabilities.Length > 0,
            "Parameter relativeProbabilities is null or empty");

        int total = 0;

        foreach (int prob in relativeProbabilities)
        {
            total += prob;
        }

        return total;
    }

    public static int RandomIndex(int[] relativeProbabilities, int totalRelativeProbability)
    {
        Debug.Assert(relativeProbabilities != null && relativeProbabilities.Length > 0,
            "Parameter relativeProbabilities is null or empty");
        Debug.Assert(totalRelativeProbability >= 0,
            "Parameter totalRelativeProbability is negative");

        int num = Random.Range(0, totalRelativeProbability + 1);
        int total = 0;

        for (int i = 0; i < relativeProbabilities.Length; ++i)
        {
            if (total >= num)
                return i;

            total += relativeProbabilities[i];
        }

        return relativeProbabilities.Length - 1;
    }
}
