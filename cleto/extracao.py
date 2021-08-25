def estEstatisticos(conjunto):
	return [valorEsperado(conjunto), dispersao(conjunto), grauSimetria(conjunto)]

def valorEsperado(conjunto):
	res = 0

	for i in conjunto:
		res += i

	return res/len(conjunto)

def dispersao(conjunto):
	v = valorEsperado(conjunto)
	res = 0

	for i in conjunto:
		res += pow((i-v),2)

	return res/len(conjunto)

def grauSimetria(conjunto):
	v = valorEsperado(conjunto)
	res = 0

	for i in conjunto:
		res += pow((i-v),3)

	d = pow(dispersao(conjunto), 3/2)

	res /= (len(conjunto) * d)

	return res

print(estEstatisticos([2,5,8,9]))