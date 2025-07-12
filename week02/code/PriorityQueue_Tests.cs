using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue these value/priority set in the shown order: "apple"/2, "lime"/1, "orange"/3
    // Then Dequeued in the priority defined by the "priority" attribute.
    // Expected Result: orange, apple, lime.
    // Defect(s) Found: The "Dequeue" function doesn't remove the value from the queue. Also, the function was not looping through the whole queue to find the index of the highest priority item.
    public void TestPriorityQueue_1()
    {

        var apple = new PriorityItem("apple", 2);
        var lime = new PriorityItem("lime", 1);
        var orange = new PriorityItem("orange", 3);

        string[] expectedResult = ["orange", "apple", "lime"];

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(apple.Value, apple.Priority);
        priorityQueue.Enqueue(lime.Value, lime.Priority);
        priorityQueue.Enqueue(orange.Value, orange.Priority);

        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], value);
            i++;
        }

    }

    [TestMethod]
    // Scenario: Enqueue these value/priority set in the shown order: "apple"/2, "lime"/1, "orange"/3. 
    // It should Enqueue each item to the back of the queue. 
    // Expected Result: [apple (Pri:2), lime (Pri:1), orange (Pri:3)]
    // Defect(s) Found: None 
    public void TestPriorityQueue_2()
    {

        var apple = new PriorityItem("apple", 2);
        var lime = new PriorityItem("lime", 1);
        var orange = new PriorityItem("orange", 3);

        string expectedResult = "[apple (Pri:2), lime (Pri:1), orange (Pri:3)]";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(apple.Value, apple.Priority);
        priorityQueue.Enqueue(lime.Value, lime.Priority);
        priorityQueue.Enqueue(orange.Value, orange.Priority);

        Assert.AreEqual(expectedResult, priorityQueue.ToString());

    }

    [TestMethod]
    // Scenario: Enqueue these value/priority set in the shown order: "apple"/2, "lime"/1, "orange"/3, "lemon"/2
    // Then Dequeued in the priority defined by the "priority" attribute. Also, if two items are found that have the same priority remove the item closest to the front of the queue.
    // Expected Result: "orange", "apple", "lemon", "lime".
    // Defect(s) Found: If the "Dequeue" function found two items with the same priority level it would remove the item that was closer to the back of the queue. 
    public void TestPriorityQueue_3()
    {

        var apple = new PriorityItem("apple", 2);
        var lime = new PriorityItem("lime", 1);
        var orange = new PriorityItem("orange", 3);
        var lemon = new PriorityItem("lemon", 2);

        string[] expectedResult = ["orange", "apple", "lemon", "lime"];

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(apple.Value, apple.Priority);
        priorityQueue.Enqueue(lime.Value, lime.Priority);
        priorityQueue.Enqueue(orange.Value, orange.Priority);
        priorityQueue.Enqueue(lemon.Value, lemon.Priority);

        int i = 0;
        while (priorityQueue.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], value);
            i++;
        }

    }

    [TestMethod]
    // Scenario: The queue is empty and it should return an error;
    // Expected Result: "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }

    }
    // Add more test cases as needed below.
}