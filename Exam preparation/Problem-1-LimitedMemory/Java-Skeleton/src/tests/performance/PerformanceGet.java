package tests.performance;

import junit.framework.Assert;
import main.LimitedMemoryCollection;
import org.junit.Test;
import org.junit.experimental.categories.Category;
import org.omg.Messaging.SYNC_WITH_TRANSPORT;
import tests.helpers.BaseTest;
import tests.types.PerformanceTests;

public class PerformanceGet extends BaseTest {

    protected static final int DEFAULT_CAPACITY = 100000;

    @Test
    @Category(PerformanceTests.class)
    public void PerformanceGet_WithExistingKeyWith100000Elements_ShouldReturnElementFast()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(DEFAULT_CAPACITY);

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            collection.set(i + "", i);
        }

        long start = System.currentTimeMillis();
        int item = collection.get("99999");
        long end = System.currentTimeMillis();
        long diff = end - start;
        Assert.assertTrue(diff <= 50);

        Assert.assertEquals(99999, item);
    }

    @Test
    @Category(PerformanceTests.class)
    public void PerformanceGet_WithExistingKeyWith80000Elements_ShouldReturnElementFast()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(DEFAULT_CAPACITY);

        for (int i = 1; i <= 80000; i++)
        {
            collection.set(i + "", DEFAULT_CAPACITY - i);
        }

        long start = System.currentTimeMillis();
        int item = collection.get("75000");
        long end = System.currentTimeMillis();
        long diff = end - start;
        Assert.assertTrue(diff <= 50);

        Assert.assertEquals(DEFAULT_CAPACITY - 75000, item);
    }

    @Test
    @Category(PerformanceTests.class)
    public void PerformanceGet_WithNonExistingKeyWith100000Elements_ShouldReturnKeyNotFoundFast()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(DEFAULT_CAPACITY);

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            collection.set(i + "", i);
        }

        long start = System.currentTimeMillis();
        try
        {
            collection.get("100001");
            Assert.fail();
        }
        catch (IllegalArgumentException ex)
        {
            //Expected
            long end = System.currentTimeMillis();
            long diff = end - start;
            Assert.assertTrue(diff <= 50);
        }
    }

    @Test
    @Category(PerformanceTests.class)
    public void PerformanceGet_With100000GetCalls()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(DEFAULT_CAPACITY);

        for (int i = 1; i <= DEFAULT_CAPACITY; i++)
        {
            collection.set(i + "", i);
        }

        long start = System.currentTimeMillis();
        for (int i = 1; i <= DEFAULT_CAPACITY; i++)
        {
            Assert.assertEquals(i, (int) collection.get(i + ""));
        }
        long end = System.currentTimeMillis();
        long diff = end - start;
        Assert.assertTrue(diff <= 350);
    }

    @Test
    @Category(PerformanceTests.class)
    public void PerformanceGet_With100000GetCallsReversed()
    {
        LimitedMemoryCollection<String, Integer> collection = new LimitedMemoryCollection<>(DEFAULT_CAPACITY);

        for (int i = 1; i <= DEFAULT_CAPACITY; i++)
        {
            collection.set(i + "", i);
        }

        long start = System.currentTimeMillis();
        for (int i = DEFAULT_CAPACITY; i > 0; i--)
        {
            Assert.assertEquals(i, (int) collection.get(i + ""));
        }

        long end = System.currentTimeMillis();
        long diff = end - start;
        Assert.assertTrue(diff <= 350);
    }
}
