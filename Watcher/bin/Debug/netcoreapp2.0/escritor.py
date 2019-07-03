brasil='\nJun 16 15:33:05 portal.inf.ufsm.br sshd[27156]: Accepted publickey for rtrindade from 179.217.246.223 port 48720 ssh2: RSA SHA256:X15PDVHhl8jR5EGDjegvnFZNyAtzVmQKcjD68k6H72A'
china='\nJun 16 22:21:27 portal.inf.ufsm.br sshd[31991]: Accepted keyboard-interactive/pam for lferreira from 218.107.132.66 port 36816 ssh2'
uk='\nJun 17 00:45:19 portal.inf.ufsm.br sshd[33080]: Accepted publickey for jvlima from 213.228.229.96 port 53968 ssh2: RSA SHA256:JIWkccfDjVZJn4YoDObSDYfU3rmL/kGnn53D/Sy6Y/k'
franca='\nJun 17 00:45:19 portal.inf.ufsm.br sshd[33080]: Accepted publickey for jfgez from 176.31.84.249 port 53968 ssh2: RSA SHA256:JIWkccfDjVZJn4YoDObSDYfU3rmL/kGnn53D/Sy6Y/k'
f = open('log.txt', 'w')
for x in range(0, 3):
	f.write(brasil)
for x in range(0, 4):
	f.write(china)
for x in range(0, 10):
	f.write(brasil)
for x in range(0, 2):
	f.write(uk)
for x in range(0, 8):
	f.write(brasil)
for x in range(0, 1):
	f.write(franca)
f.close()
