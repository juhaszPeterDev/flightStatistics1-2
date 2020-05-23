import re

with open('count-x-y-w.txt','r') as szoveg:
  data = szoveg.read().replace('\n','')

print(data)

x = len(re.findall('X',data))
y = len(re.findall('Y',data))
w = len(re.findall('W',data))
print('X+Y-W=',x+y-w)
