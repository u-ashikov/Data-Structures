package main;

public interface ILimitedMemoryCollection<K, V> extends Iterable<Pair<K, V>> {

    V get(K key);

    void set(K key, V value);

    int getCapacity();

    int getCount();
}
