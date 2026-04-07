📌 Project Background

This application was originally developed and completed in early 2015. 
It is designed to extract tree cross-sections from photogrammetric and LiDAR point clouds, with a primary focus on estimating Diameter at Breast Height (DBH) within the standard range of 1.27–1.33 m.

🌳 Functionality
Extracts tree stem cross-sections from point cloud data
Optimized for DBH measurement at breast height
Supports both photogrammetric and LiDAR-derived point clouds

Additionally, the application allows the extraction of cross-sections at custom heights along the tree stem. This can be achieved by modifying the height parameter directly in the source code:

📄 File: Form1.cs
⚙️ Adjust the height value to match your specific analysis requirements
⚙️ Usage Notes
▶️ Easy to use: Simply run the application and follow the on-screen instructions
📏 Pre-processing required: Ensure your point cloud is normalized along the Z-axis (height) before importing
You can use tools such as LasTools, CloudCompare, or similar software
📂 Supported formats:
.txt
.csv
.xyz
💾 Save your work: Always click the Save button before closing the application to export results
⚡ Performance: Processing time depends on file size, but the application is optimized to handle relatively large point clouds efficiently

Application Domain: Forestry

Contributors: Dimitris Panagiotidis

 
