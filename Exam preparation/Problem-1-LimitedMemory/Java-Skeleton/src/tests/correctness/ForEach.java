package tests.correctness;


import main.LimitedMemoryCollection;
import main.Pair;
import org.junit.Assert;
import org.junit.Test;
import org.junit.experimental.categories.Category;
import tests.helpers.BaseTest;
import tests.types.CorrectnessTests;

public class ForEach extends BaseTest {
    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_ShouldEnumerate_CountElements()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(4);
        String[] keys = { "A", "B", "C", "D"};

        for (String key : keys)
        {
            collection.set(key, 1);
        }

        int count = 0;
        for (Pair<String, Integer> record : collection)
        {
            count++;
        }

        Assert.assertEquals(count, collection.getCount());
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_ShouldReturn_InOrder_MostRecentlyRequested()
    {
        LimitedMemoryCollection<Character, Integer> collection = new LimitedMemoryCollection<>(4);
        Character[] keys = { 'A', 'B', 'C', 'D' };

        for (Character key : keys) {
            collection.set(key, key + 1);
        }

        int order = collection.getCount() - 1;
        for (Pair<Character, Integer> record : collection)
        {
            Assert.assertEquals(record.getKey(), keys[order]);
            Assert.assertEquals((long) record.getValue(), keys[order] + 1);
            order--;
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_Should_Return_InOrder_MostRecentlyRequested_2()
    {
        LimitedMemoryCollection<Character, Integer> collection = new LimitedMemoryCollection<>(4);
        Character[] keys = { 'A', 'B', 'C', 'D' };

        for (Character key : keys) {
            collection.set(key, key + 1);
        }

        collection.get(keys[1]);
        Character[] expectedOrder = { keys[0], keys[2], keys[3], keys[1] };

        int order = collection.getCount() - 1;
        for (Pair<Character, Integer> record : collection)
        {
            Assert.assertEquals(record.getKey(), expectedOrder[order]);
            Assert.assertEquals((long) record.getValue(), expectedOrder[order] + 1);
            order--;
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_Should_Return_InOrder_MostRecentlyRequested_3()
    {
        LimitedMemoryCollection<Character, Integer> collection = new LimitedMemoryCollection<>(4);
        Character[] keys = { 'A', 'B', 'C', 'D' };

        for (Character key : keys) {
            collection.set(key, key + 1);
        }

        collection.get(keys[2]);
        collection.get(keys[0]);
        Character[] expectedOrder = { keys[1], keys[3], keys[2], keys[0] };

        int order = collection.getCount() - 1;
        for (Pair<Character, Integer> record : collection)
        {
            Assert.assertEquals(record.getKey(), expectedOrder[order]);
            Assert.assertEquals((long) record.getValue(), expectedOrder[order] + 1);
            order--;
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_Should_Return_InOrder_MostRecentlyRequested_4()
    {
        LimitedMemoryCollection<Character, Integer> collection = new LimitedMemoryCollection<>(4);
        Character[] keys = { 'A', 'B', 'C', 'D' };

        for (Character key : keys) {
            collection.set(key, key + 1);
        }

        collection.set(keys[1], 5);
        Character[] expectedOrder = { keys[0], keys[2], keys[3], keys[1] };

        int order = collection.getCount() - 1;
        for (Pair<Character, Integer> record : collection)
        {
            Assert.assertEquals(record.getKey(), expectedOrder[order]);
            order--;
        }
    }

    @Test
    @Category(CorrectnessTests.class)
    public void Foreach_Should_Return_InOrder_MostRecentlyRequested_5()
    {
        LimitedMemoryCollection<Character, Integer> collection = new LimitedMemoryCollection<>(5);
        Character[] keys = { 'A', 'B', 'C', 'D', 'E' };

        for (Character key : keys) {
            collection.set(key, key + 1);
        }

        collection.set(keys[0], 5);
        collection.set(keys[3], 5);
        collection.get(keys[0]);

        Character[] expectedOrder = { keys[1], keys[2], keys[4], keys[3], keys[0] };

        int order = collection.getCount() - 1;
        for (Pair<Character, Integer> record : collection)
        {
            Assert.assertEquals(record.getKey(), expectedOrder[order]);
            order--;
        }
    }
}
