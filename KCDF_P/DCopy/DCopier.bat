net use \\192.168.0.249 /USER:Administrator Admin7654321
net use \\192.168.0.250 /USER:Administrator Admin987654321
START /BELOWNORMAL ROBOCOPY "\\192.168.0.250\All Uploads" "\\192.168.0.249\testCopy" /mir
