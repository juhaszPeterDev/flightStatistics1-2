with open('caesar.txt','r') as szoveg:
  data = szoveg.read()

print(data)
eltolas = 10  # decimálisan 16
ABC = "abcdefghijklmnopqrstuvwxyz"
dict = {}

# szótár (dictionary) generálás
def dict_gen(elt, abc):
  dic = {}
  abchossz = len(abc)
  kx = elt
  for k in abc:
    dic[k] = abc[kx]
    dic[k.upper()] = abc[kx].upper()
    kx += 1 
    if kx > abchossz - 1:
      kx = kx - abchossz
  return dic



# generáljunk egy 10 eltolásos dictionary-t
dict=dict_gen(10,ABC)
# nézzük meg mi van benne
print(dict)
# kiírunk egy csodaszép fejlécet
print ('\n============',10,'=============\n')

# karakterenként végig megyünk a 'versen'
# a karaktereket kulcsnak (key) használjuk a dictionary-ban
for b in data:
  if b in dict.keys():
    print(dict[b], end='')
  else:
    print(b, end='')
