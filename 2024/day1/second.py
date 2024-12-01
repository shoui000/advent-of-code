f = open('data.txt', 'r')

left = []
right = []
for l in f.readlines():
    l = l.strip()

    t, y  = l.split('   ')
    left.append(int(t))
    right.append(int(y))

f.close()

sum = 0
for num in left:

    qt = 0
    for nnm in right:
        if nnm == num:
            qt += 1
    
    sum += qt * num

print(sum)
