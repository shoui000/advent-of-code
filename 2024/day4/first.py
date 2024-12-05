import re
f = open('data.txt', 'r')

matrix = []
sum = 0

for l in f.readlines():
    l = l.strip()

    matrix.append([])
    for c in l:
        matrix[-1].append(c)

    x = re.findall(r"(XMAS)", l)
    z = re.findall(r"(SAMX)", l)
    sum += len(x)
    sum += len(z)

f.close()

cMatrix = []
for i, l in enumerate(matrix):
    for j, e in enumerate(l):
        if len(cMatrix)-1 < j:
            cMatrix.append([])
        cMatrix[j].append(e)

for l in cMatrix:
    l = ''.join(l)
    x = re.findall(r"(XMAS)", l)
    z = re.findall(r"(SAMX)", l)
    sum += len(x)
    sum += len(z)

dgMatrix = []
for i, l in enumerate(matrix):
    index = len(l)
    dgMatrix.append([''] * (index - 1 - i))

    for c in l:
        dgMatrix[-1].append(c)

    for x in range(0, i):
        dgMatrix[-1].append('')

cdgMatrix = []
for i, l in enumerate(dgMatrix):
    for j, e in enumerate(l):
        if len(cdgMatrix)-1 < j:
            cdgMatrix.append([])
        cdgMatrix[j].append(e)

for l in cdgMatrix:
    l = ''.join(l)
    x = re.findall(r"(XMAS)", l)
    z = re.findall(r"(SAMX)", l)
    sum += len(x)
    sum += len(z)

rdgMatrix = []
for i, l in enumerate(matrix):
    index = len(l)
    rdgMatrix.append([''] * (i))
    
    for c in l:
        rdgMatrix[-1].append(c)

    for x in range(0, index - 1 - i):
        rdgMatrix[-1].append('')

crdgMatrix = []
for i, l in enumerate(rdgMatrix):
    for j, e in enumerate(l):
        if len(crdgMatrix)-1 < j:
            crdgMatrix.append([])
        crdgMatrix[j].append(e)

for l in crdgMatrix:
    l = ''.join(l)
    x = re.findall(r"(XMAS)", l)
    z = re.findall(r"(SAMX)", l)
    sum += len(x)
    sum += len(z)

print(sum)
