f = open('data.txt', 'r')

def quicksort(array, b=0, e=None):
    if e is None:
        e = len(array)-1
    if b < e:
        p = partition(array, b, e)
        quicksort(array, b, p-1)
        quicksort(array, p+1, e)

def partition(array, b, e):
    pivot = array[e]
    i = b
    for j in range(b, e):
        if array[j] <= pivot:
            array[j], array[i] = array[i], array[j]
            i += 1
    array[i], array[e] = array[e], array[i]
    return i

left = []
right = []
for l in f.readlines():
    l = l.strip()

    t, y  = l.split('   ')
    left.append(int(t))
    right.append(int(y))

f.close()

quicksort(left)
quicksort(right)

sum = 0
for i in range(0, len(left)):
    diff = abs(left[i] - right[i])

    sum += diff

print(sum)
