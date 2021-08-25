from sklearn.svm import SVC
from sklearn.metrics import accuracy_score

clf = SVC(kernel='linear')
x_train = [[0, 0], [0, 1], [1, 0], [1, 1]]
y_train = [0, 1, 1, 0]
x_test  = [[2, 2], [2, 1]]
y_test  = [0, 1]
clf.fit(x_train,y_train)
y_pred = clf.predict(x_test)
print(y_pred)
print(accuracy_score(y_test,y_pred))
