import mne
import numpy as np
from scipy.signal import butter, lfilter, freqz
import matplotlib.pyplot as plt

def butter_bandpass(lowcut, highcut, fs, order=5):
    nyq = 0.5 * fs
    low = lowcut / nyq
    high = highcut / nyq
    b, a = butter(order, [low, high], btype='band')
    return b, a


def butter_bandpass_filter(data, lowcut, highcut, fs, order=5):
    b, a = butter_bandpass(lowcut, highcut, fs, order=order)
    y = lfilter(b, a, data)
    return y

def filter(eeg):
	fs = len(eeg)
	lowcut = 9.0
	highcut = 12.0

	plt.figure(1)
	plt.plot(eeg)

	y = butter_bandpass_filter(eeg, lowcut, highcut, fs, order=1)

	plt.plot(y)

	plt.show()

raw = mne.io.read_raw_gdf("A01E.gdf")
data = raw.to_data_frame()

filter(data.iloc[:,5].to_numpy())


'''
MNE
raw = mne.io.read_raw_gdf("A01E.gdf")

print(raw.get_data())
raw.plot(n_channels=1,block=True)
print(raw.describe(data_frame=True))
print(raw.to_data_frame())

data = raw.to_data_frame()
print(data)
'''