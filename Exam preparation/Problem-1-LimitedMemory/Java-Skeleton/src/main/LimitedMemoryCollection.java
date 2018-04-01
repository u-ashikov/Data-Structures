package main;


import java.util.Iterator;

public class LimitedMemoryCollection<K, V> implements ILimitedMemoryCollection<K, V> {

    public LimitedMemoryCollection(int capacity) {
    }

    @Override
    public V get(K key) {
        return null;
    }

    @Override
    public void set(K key, V value) {

    }

    @Override
    public int getCapacity() {
        return 0;
    }

    @Override
    public int getCount() {
        return 0;
    }

    @Override
    public Iterator<Pair<K, V>> iterator() {
        return null;
    }
}
