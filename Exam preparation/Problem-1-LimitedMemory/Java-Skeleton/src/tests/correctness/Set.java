package tests.correctness;

import junit.framework.Assert;
import main.LimitedMemoryCollection;
import org.junit.Test;
import org.junit.experimental.categories.Category;
import tests.helpers.BaseTest;
import tests.types.CorrectnessTests;

public class Set extends BaseTest {
    @Test
    @Category(CorrectnessTests.class)
    public void Set_WithAnExistingKey_ShouldSetNewValue()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        collection.set("A", 5);

        collection.set("A", 3);

        Assert.assertEquals(3, (int) collection.get("A"));
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_WithMultipleExistingKeys_ShouldSetElementsCorrectly()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        for (int i = 0; i < collection.getCapacity(); i++)
        {
            String letter = ((char)(65 + i)) + "";
            collection.set(letter, i);
        }

        for (int i = 0; i < collection.getCapacity(); i++)
        {
            String letter = ((char)(65 + i)) + "";
            collection.set(letter, i + 1);
        }

        for (int i = 0; i < collection.getCapacity(); i++)
        {
            String letter = ((char)(65 + i)) + "";
            Assert.assertEquals((int) collection.get(letter), i + 1);
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_MissingKey_CapacityLeft_ShouldAddElement()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);
        try
        {
            collection.get("A");
            Assert.fail();
        }
        catch (IllegalArgumentException ex)
        {
            //Expected
        }

        collection.set("A", 5);

        collection.get("A");
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_MissingKey_CapacityLeft_ShouldIncreaseCount()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        collection.set("A", 5);
        Assert.assertEquals(1, collection.getCount());
        collection.set("B", 3);
        Assert.assertEquals(2, collection.getCount());
    }

    @Test
            @Category(CorrectnessTests.class)
    public void Set_ExistingKey_CapacityLeft_ShouldNotChangeCount()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        collection.set("A", 3);
        collection.set("A", 4);

        Assert.assertEquals(4, (int) collection.get("A"));
        Assert.assertEquals(1, collection.getCount());
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_ExistingKey_CapacityFull_ShouldNotChangeCount()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        for (int i = 0; i < collection.getCapacity(); i++)
        {
            String letter = ((char)(65 + i)) + "";
            collection.set(letter, i);
        }

        collection.set("A", 2);

        Assert.assertEquals(2, (int) collection.get("A"));
        Assert.assertEquals(4, collection.getCount());
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_ExistingKey_CapacityFull_ShouldNotChangeOtherElements()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        for (int i = 0; i < collection.getCapacity(); i++)
        {
            String letter = ((char)(65 + i)) + "";
            collection.set(letter, i);
        }

        collection.set("A", 2);

        for (int i = 1; i < collection.getCapacity(); i++)
        {
            // Check if records are still there
            String  letter = ((char)(65 + i)) + "";
            Assert.assertEquals(i, (int) collection.get(letter));
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Set_NewKey_CapacityFull_ShouldRemove_LongestAgoRequestedRecord()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);

        String[] keys = { "A", "C", "D", "B" };
        for (int i = 0; i < keys.length; i++)
        {
            collection.set(keys[i], 1);
        }

        collection.set("G", 1);
        for (int i = 1; i < keys.length; i++)
        {
            // Check if records are still there
            collection.get(keys[i]);
        }

        try
        {
            // Expecting exception to be thrown
            collection.get(keys[0]);
            Assert.fail("Key should be removed to make room for new key!");
        }
        catch (IllegalArgumentException ex)
        {
            // Everything is OK
        }
    }
}
