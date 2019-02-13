import pymongo as pm
import cognitive_face as CF
import requests
from io import BytesIO
from PIL import Image, ImageDraw
import glob
import tkinter as tk
from tkinter import filedialog


KEY = '19cbaf8a3a224dd687d5fe31f6ef353b'  # Replace with a valid subscription key (keeping the quotes in place).
CF.Key.set(KEY)
PERSON_GROUP_ID = 'proxykiller'
BASE_URL = 'https://centralindia.api.cognitive.microsoft.com/face/v1.0'  # Replace with your regional Base URL
CF.BaseUrl.set(BASE_URL)
result = CF.person_group.create(PERSON_GROUP_ID)
print(result)