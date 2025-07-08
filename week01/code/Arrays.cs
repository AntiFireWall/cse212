 public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Set the amount of multiples to return given in the "length" argument.
        int multiplesToFind = length;
        // Set a starting number from the "number" argument witch will be incremented after each check.
        double startingNumber = number;
        // Create a array with a length of "length" argument that will store all the numbers that are multiples of the "number" argument.
        double[] result = new double[length];
        // Loop until "multiplesToFind" reaches 0, meaning that the target amount of multiples set by "lenght" argument has been reached.
        for (int i = 0; multiplesToFind != 0; i++)
        {
            // Checks if the "startingNumber" is a multiple of "number". If "True" it will add the multiple to the "results" list and reduce "multiplesToFind" by 1.
            if (startingNumber % number == 0)
            {
                result[result.Length - multiplesToFind] = startingNumber;
                multiplesToFind--;
            }
            // Then "startingNumber" will increment by 0.5 if is positive and reduce by 0.5 if negative.  
            startingNumber = (startingNumber > 0) ? startingNumber += 0.5 : startingNumber -= 0.5;
        }
        return result; // Return the result.
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Grab the part of the list that would be sent to the start of the list if moved right by the specified amount given in "amount" argument and put it into a new "partOfList" list.
        List<int> partOfList = data.GetRange(data.Count - amount, amount);
        // Remove that part from the list.
        data.RemoveRange(data.Count - amount, amount);
        // Then grab the removed part that was stored in "partOfList" list and insert it at the beginning of the original list.
        data.InsertRange(0, partOfList);
    }
}
