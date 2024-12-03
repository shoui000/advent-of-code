f = open("data.txt", 'r')

def verify(array):
    if array != sorted(array) and array != sorted(array, reverse=True):
        return False

    for i in range(len(array)-1):
        diff = array[i] - array[i+1]

        if abs(diff) == 0 or abs(diff) > 3:
            return False

    return True

sum  = 0
for l in f.readlines():
    l = list(map(int, l.strip().split()))
    x = l.copy()

    for i in range(len(l)):
        if verify(x):
            sum += 1
            break
        else:
            x = l.copy()
            x.pop(i)
    else:
        if verify(x):
            sum += 1

print(sum)

f.close()
