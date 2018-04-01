package tests.performance;

import junit.framework.Assert;
import main.LimitedMemoryCollection;
import main.Pair;
import org.junit.Test;
import org.junit.experimental.categories.Category;
import tests.helpers.BaseTest;
import tests.types.PerformanceTests;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Random;

public class PerformanceForEach extends BaseTest {

    @Category(PerformanceTests.class)
    @Test
    public void PerformanceForEach_With100000Elements_ShouldIterateOptimally()
    {
        Random random = new Random();
        int Capacity = 100000;
        LimitedMemoryCollection<Integer,Integer> collection = new LimitedMemoryCollection<Integer, Integer>(Capacity);
        Pair<Integer,Integer>[] records = new Pair[Capacity];
        for (int i = 0; i < records.length; i++) {
            records[i] = new Pair<>(i, random.nextInt(100));

        }

        for (Pair<Integer, Integer> record : records) {
            collection.set(record.getKey(),record.getValue());
        }

        int count = 99999;

        long start = System.currentTimeMillis();

        for (Pair<Integer, Integer> record : collection) {
            Assert.assertEquals("Expected Key did not match!",records[count].getKey(), record.getKey());
            Assert.assertEquals("Expected Value did not match!",records[count--].getValue(), record.getValue());
        }

        long end = System.currentTimeMillis();
        Assert.assertTrue(end - start < 300);
    }

    @Category(PerformanceTests.class)
    @Test
    public void PerformanceForEach_With100000ElementsReversed_ShouldIterateOptimally()
    {
        int Capacity = 100000;
        LimitedMemoryCollection<Integer,Integer> collection = new LimitedMemoryCollection<Integer, Integer>(Capacity);
        Pair<Integer,Integer>[] records = new Pair[Capacity];
        for (int i = records.length-1; i >= 0; i--) {
            records[i] = new Pair<>(i,i);

        }

        for (Pair<Integer, Integer> record : records) {
            collection.set(record.getKey(),record.getValue());
        }

        Collections.reverse(Arrays.asList(records));

        int count = 0;

        long start = System.currentTimeMillis();

        for (Pair<Integer, Integer> record : collection) {
            Assert.assertEquals("Expected Key did not match!", records[count].getKey(), record.getKey());
            Assert.assertEquals("Expected Value did not match!",records[count++].getValue(), record.getValue());
        }

        long end = System.currentTimeMillis();
        Assert.assertTrue(end - start < 300);
    }

    @Category(PerformanceTests.class)
    @Test
    public void PerformanceForEach_With80000MixedElements_ShouldIterateOptimally()
    {
        int Capacity = 80000;
        LimitedMemoryCollection<Integer,Integer> collection = new LimitedMemoryCollection<Integer, Integer>(Capacity);
        ArrayList<Integer> records = new ArrayList<>();
        for (int i = 0; i < 40000; i++) {
            records.add(i);
            records.add(79999 - i);
        }

        for (Integer integer : records) {
            collection.set(integer,integer);
        }

        Collections.reverse(records);

        int count = 0;

        long start = System.currentTimeMillis();

        for (Pair<Integer, Integer> record : collection) {
            Assert.assertEquals("Expected Key did not match!",records.get(count), record.getKey());
            Assert.assertEquals("Expected Value did not match!",records.get(count++), record.getValue());
        }

        long end = System.currentTimeMillis();
        Assert.assertTrue(end - start < 300);
    }
}
